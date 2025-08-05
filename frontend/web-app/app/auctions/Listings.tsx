'use client';
import { Auction } from "@/types/index";
import AuctionCard from "./AuctionCard";
import AppPagination from "../components/AppPagination";
import { getData } from "../actions/auctionsAction";
import { useEffect, useState } from "react";
import Filters from "./Filters";
import { useParamsStore } from "@/hooks/useParamsStore";
import { useShallow } from "zustand/shallow";
import qs from 'query-string'; 
import EmptyFilter from "../components/EmptyFilter";
import { useAuctionStore } from "@/hooks/useAuctionStore";

export default function Home() {
    const [loading, setLoading] = useState(true);
     const params = useParamsStore(useShallow(state =>({ 
        pageNumber:state.pageNumber,
        pagesize: state.pageSize,
        searchTerm: state.searchTerm,
        orderBy: state.orderBy,
        filterBy: state.filterBy,
        seller: state.seller,
        winner: state.winner
    })));   

    const data = useAuctionStore(useShallow(state => ({ 
        auctions: state.auctions,
        totalCount: state.totalCount,
        pageCount: state.pageCount,
    })));

    const setData = useAuctionStore(state => state.setData);


    const setParams = useParamsStore(state => state.setParams);
    const url = qs.stringifyUrl({url:'', query: params}, {skipEmptyString: true});

    function setPageNumber(pageNumber: number){
        setParams({pageNumber});
    }

    useEffect(() => {
        getData(url).then(data => {
            setData(data); 
            setLoading(false);
        })},
        [url, setData]);

    if(loading) return <h3>Loading... </h3>
     return (
        <>
        <Filters/>
        {data.totalCount === 0 ? (
            <EmptyFilter showReset/>
        ): (
            <>
                <div className="grid grid-cols-4 gap-6">
            {data && data.auctions.map((auction: Auction) => (
                <AuctionCard key={auction.id} auction={auction} />
            ))}
        </div>
        <div className="flex justify-center mt-4">
            {/* Pagination component can be added here if needed */}
            {/* <AppPagination currentPage={1} pageCount={data.pageCount} /> */}
            <AppPagination pageChanged={setPageNumber} currentPage={params.pageNumber} pageCount={data.pageCount} />
        </div></>
        )}
    
        </>
    );
} 