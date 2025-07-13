
import { getDetailedViewData } from '@/app/actions/auctionsAction';
import Heading from '@/app/components/Headings';
import React from 'react'
import CountDownTimer from '../../CountDownTimer';
import CardImage from '../../CardImage';
import DetailedSpecs from './DetailedSpecs';
import EditButton from './EditButton';
import { getCurrentUser } from '@/app/actions/authActions';
import DeleteButton from './DeleteButton';

export default async function Details({params}: {params:Promise<{id: string}>}) {
  const { id } = await params;
  const data = await getDetailedViewData(id);
  const user = await getCurrentUser();
  return (
    <>
    <div className='flex justify-between'>
      <div className='flex items-center gap-3'>
        <Heading title={`${data.make} ${data.model}`} />
        {user?.username === data.seller && (
            <>
            <EditButton id={data.id} />  
            <DeleteButton id={data.id} />
            </>
        )}
        
      </div>
      <Heading title={`${data.make} ${data.model}`} />
      <div className='flex gap-3'>
        <h3 className='text-2xl font-semibold'>Time remaining</h3>
        <CountDownTimer auctionEnd={data.auctionEnd} />
      </div>
    </div>
    <div className='grid grid-cols-2 gap-6 mt-3'>
      <div className='relative w-full bg-gray-200 aspect-[4/3]'>
         <CardImage imageUrl={data.imageUrl} />
      </div>
      <div className='border-2 rounded-lg p-2 bg-gray-200'>
        <Heading title='Bids' />
      </div>
      </div>
          <div className="mt-3 grid grid-cols-1 rounded-lg">
              <DetailedSpecs auction={data} />
          </div>
    </>
  )
}
