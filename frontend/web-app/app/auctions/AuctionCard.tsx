import React from 'react'
import CountDownTimer from './CountDownTimer'
import CardImage from './CardImage'
import Link from 'next/link'

type Props=  {
    auction: any
}

export default function AuctionCard({auction}: Props) {
  return (
    <Link href={`/auctions/details/${auction.id}`} className='block p-4 bg-white shadow-md rounded-lg hover:shadow-lg transition-shadow duration-200'>
        <div className= "relative w-full bg-gray-200 aspect-video rounded-lg overflow-hidden">
            <CardImage imageUrl={auction.imageUrl} />
            <div className='absolute bottom-2 left-2'>
              <CountDownTimer auctionEnd={auction.auctionEnd}/>
            </div>
        </div>
        <div className='flex justify-between items-center mt-4'>
          <h3 className='text-gray-700'>{auction.make} {auction.model}</h3>
          <p className='font-semibold text-sm'>{auction.year}</p>
        </div>
    </Link>
    
  )
}
